[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    $Title,
    [System.DateOnly]
    $Date = ((Get-Date).ToLongDateString())
)

$slug = $Title.ToLowerInvariant().Trim().Replace(' ', '-')
$Destination = "$PSScriptRoot/content/posts/$($Date.Year)/$slug.md"
$Template = Get-Content "$PSScriptRoot/templates/til.md" -Raw
$Output = $Template -f $Title, $Date.ToShortDateString()
New-Item -Path $Destination -Force
Set-Content -Path $Destination -Value $Output -Encoding utf8NoBOM

if (Get-Command Open-EditorFile) {
    Open-EditorFile $Destination
}
