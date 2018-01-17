$SolutionDir = Split-Path $Script:MyInvocation.MyCommand.Path
cd $SolutionDir
Write-Host "$pwd"

[xml]$ProjFile = Get-Content -Path G1ANT.Addon.MSOffice\G1ANT.Addon.MSOffice.csproj
$ref = @()
$ProjFile.Project.ItemGroup.Reference | foreach{$ref+=$_}
$toEmbed = @()
foreach($r in $ref)
{
	if($r.HintPath -and (!$r.SpecificVersion -or ($r.SpecificVersion -eq $true)))
	{
		$toEmbed += $r
	}
}
Write-Host $toEmbed