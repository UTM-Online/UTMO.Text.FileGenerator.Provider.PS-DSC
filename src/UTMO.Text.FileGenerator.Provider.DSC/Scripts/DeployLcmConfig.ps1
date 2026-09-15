param
(
    [Parameter(Mandatory=$true)]
    [string]$Path,
    [Parameter(Mandatory=$false)]
    [string]$Username = "homenet\HN0-SVC-AZAutomatRW",
    [Parameter(Mandatory=$true)]
    [string]$Password
)

Write-Output "Gathering a list of machines"

$machines = Get-ChildItem -Path $Path | ?{$_.Extension -eq ".mof"} | %{$_.Name.TrimEnd(".meta.mof".ToCharArray())}

function Test-IsLocalMachine {
    param(
        [string]$ComputerName
    )

    if ([string]::IsNullOrWhiteSpace($ComputerName)) {
        return $false
    }

    $candidateNames = @(
        $ComputerName.Trim(),
        $ComputerName.Trim().TrimEnd('.'),
        $ComputerName.Split('.')[0]
    ) | Where-Object { $_ }

    $localNames = @(
        $env:COMPUTERNAME,
        [System.Net.Dns]::GetHostName(),
        $env:COMPUTERNAME.Split('.')[0],
        ([System.Net.Dns]::GetHostName()).Split('.')[0],
        "localhost",
        "127.0.0.1"
    ) | ForEach-Object { $_.Trim().TrimEnd('.') } | Where-Object { $_ }

    return (($candidateNames | Where-Object { $_ -in $localNames }).Count -gt 0)
}

Write-Output "Single Threaded Validation"

Write-Output "Validating Endpoint Availability"

$localMachines = @()
$remoteMachines = @()

foreach($machine in $machines)
{
    if (Test-IsLocalMachine -ComputerName $machine)
    {
        $localMachines += $machine
        Write-Host "Local host detected for ${machine}; skipping WSMan validation." -ForegroundColor Yellow
        continue
    }

    Write-Host "Testing Host ${machine}: " -NoNewline
    $TestResult = $null
    try
    {
        $TestResult = Test-WSMan -ComputerName $machine -ErrorAction Stop
    }
    catch
    {
        $TestResult = $_
    }

    if($null -eq $TestResult -or $TestResult -is [System.Management.Automation.ErrorRecord])
    {
        Write-Host "Fail!" -ForegroundColor Red
        Write-Host "Failed Test Results:"
        $TestResult
        Remove-Item -Path "$Path\$machine.meta.mof" -Force -Confirm:$false
    }
    else
    {
        $remoteMachines += $machine
        Write-Host "Succeeded!" -ForegroundColor Green
    }
}

Write-Output "Generating Push Credentials"

$secPass = ConvertTo-SecureString -String $Password -AsPlainText -Force
$creds = New-Object System.Management.Automation.PSCredential ($Username,$secPass)

Write-Output "Validating Push Credentials"

# Get current domain using logged-on user's credentials
$CurrentDomain = "LDAP://" + ([ADSI]"").distinguishedName
$domain = New-Object System.DirectoryServices.DirectoryEntry($CurrentDomain,$UserName,$Password)

if ($domain.name -eq $null)
{
    write-host "Authentication failed - please verify the username and password."
    [Environment]::Exit(2)
}
else
{
    write-host "Successfully authenticated with domain $($domain.name)"
}

Write-Output "Setting DSC LCM"

if ($localMachines.Count -gt 0)
{
    foreach($machine in $localMachines)
    {
        Set-DscLocalConfigurationManager -ComputerName $machine -Path $Path -Force -Verbose
    }
}

if ($remoteMachines.Count -gt 0)
{
    Set-DscLocalConfigurationManager -ComputerName $remoteMachines -Path $Path -Credential $creds -Force -Verbose
}

Write-Output "Deployment Completed Successfully"
