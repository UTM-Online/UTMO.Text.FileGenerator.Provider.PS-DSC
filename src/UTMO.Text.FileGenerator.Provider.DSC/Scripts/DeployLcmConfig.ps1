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

Write-Output "Single Threaded Validation"

Write-Output "Validating Endpoint Availability"

foreach($machine in $machines)
{
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
        $machines = $machines | ?{$_ -ne $machine}
        Write-Host "Fail!" -ForegroundColor Red
        Write-Host "Failed Test Results:"
        $TestResult
        Remove-Item -Path "$Path\$machine.meta.mof" -Force -Confirm:$false
    }
    else
    {
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

Set-DscLocalConfigurationManager -ComputerName $machines -Path $Path -Credential $creds -Force -Verbose

Write-Output "Deployment Completed Successfully"
