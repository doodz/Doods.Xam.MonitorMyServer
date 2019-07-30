 Param(

     [Parameter(Mandatory=$True)]
    [string]$ProjectDir,
    [Parameter(Mandatory=$True)]
    [string]$SolutionDir,
    [Parameter(Mandatory=$True)]
    [string]$TargetDir,
	 [Parameter(Mandatory=$True)]
    [string]$SolutionPath
)
[Reflection.Assembly]::LoadWithPartialName('System.IO.Compression.FileSystem')
<#$nupkgs = Get-ChildItem -Recurse -Filter *.nupkg -Path "$SolutionDir\packages" 
$nuspecs = $nupkgs | %{ [IO.Compression.ZipFile]::OpenRead($_.FullName).Entries | where {$_.Fullname.EndsWith('.nuspec')} } 
$metadata = $nuspecs | %{ 
    ([xml]([System.IO.StreamReader]$_.Open()).ReadToEnd()) | %{New-Object PSObject -Property @{
        Version = $_.package.metadata.version
        Authors = $_.package.metadata.authors 
        Title =  IF ([string]::IsNullOrWhitespace($_.package.metadata.title)){$_.package.metadata.id} else {$_.package.metadata.title}
        LicenseUrl  = $_.package.metadata.licenseUrl
    }}
} 
$metadata | %{ '{0} {1}{4}Autor(en): {2}{4}Lizenz: {3}{4}{4}' -f $_.Title, $_.Version, $_.Authors, $_.LicenseUrl, [Environment]::NewLine } | Out-File "$ProjectDir\Resx\ThirdPartyLicenseOverview.txt"
#>
#Get-Content $SolutionPath | where { $_ -match "Project.+, ""(.+csproj)""," }| Out-File "$ProjectDir\Resx\test.txt"
Get-Content $SolutionPath | where { $_ -match "Project.+, ""(.+csproj)""," } | foreach { $matches[1] } | % {Get-Content $SolutionDir$_ |  where { $_ -match "<PackageReference Include=.+Version="} } | Sort-Object -Unique | Out-File "$ProjectDir\Resx\ThirdPartyLicenseOverview_doods.txt"


#Get-Content $SolutionPath | where { $_ -match "Project.+, ""(.+)""," } | foreach { $matches[1] } | % {Get-Content $SolutionDir$_ | Find "<PackageReference Include" } | Sort-Object -Unique | Out-File "$ProjectDir\Resx\ThirdPartyLicenseOverview_doods.txt"


# powershell.exe -ExecutionPolicy Bypass -File $(ProjectDir)\Resx\Tools\PreBuildScript.ps1  $(ProjectDir) $(SolutionDir) $(TargetDir) $(SolutionPath)
# "powershell.exe -ExecutionPolicy Bypass -File C:\GitHub\Doods.Xam.MonitorMyServer\src\MonitorMyServer\\Resx\Tools\PreBuildScript.ps1  C:\GitHub\Doods.Xam.MonitorMyServer\src\MonitorMyServer\ C:\GitHub\Doods.Xam.MonitorMyServer\ C:\GitHub\Doods.Xam.MonitorMyServer\src\MonitorMyServer\bin\Debug\netstandard2.0\ C:\GitHub\Doods.Xam.MonitorMyServer\Doods.Xam.MonitorMyServer.sln