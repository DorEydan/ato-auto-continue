param(
    [string]$GameDirectory = 'D:\Program Files\Steam\steamapps\common\Across the Obelisk',
    [string]$BepInExDirectory = (Join-Path $PSScriptRoot '..\..\BepInEx')
)
$ErrorActionPreference = 'Stop'
$managed = Join-Path $GameDirectory 'AcrossTheObelisk_Data\Managed'
$sdk = (& dotnet --list-sdks | Select-Object -Last 1)
if ($sdk -notmatch '^([^ ]+) \[(.+)\]') { throw 'Install a .NET SDK to build.' }
$compiler = Join-Path $Matches[2] "$($Matches[1])\Roslyn\bincore\csc.dll"
$refs = @('mscorlib.dll','System.dll','System.Core.dll','UnityEngine.dll','UnityEngine.CoreModule.dll','Assembly-CSharp.dll','netstandard.dll') | ForEach-Object { '/reference:' + (Join-Path $managed $_) }
$refs += '/reference:' + (Join-Path $BepInExDirectory 'core\BepInEx.dll')
$outDir = Join-Path $BepInExDirectory 'plugins\AutoContinue'
New-Item -ItemType Directory -Force $outDir | Out-Null
$out = Join-Path $outDir 'AutoContinue.dll'
& dotnet $compiler /nologo /target:library /nostdlib+ /optimize+ /deterministic+ "/out:$out" $refs "$PSScriptRoot\Plugin.cs"
if ($LASTEXITCODE -ne 0) { throw 'Compilation failed.' }
Write-Output "Built $out"
