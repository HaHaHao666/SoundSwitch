# Build a portable, self-contained single-file SoundSwitch.exe into .\publish

$root = $PSScriptRoot
$version = (Get-Content "$root\version.txt" -Raw).Trim()

dotnet publish "$root\SoundSwitch\SoundSwitch.csproj" `
    -c Release `
    -r win-x64 `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:EnableCompressionInSingleFile=true `
    -p:SolutionDir="$root\" `
    -o "$root\publish"

if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

$target = "$root\publish\SoundSwitchWidget-v$version.exe"
Move-Item "$root\publish\SoundSwitch.exe" $target -Force
(Get-FileHash $target -Algorithm SHA256).Hash | Out-File "$target.SHA256"

Write-Output "Output: $target"
