$global:OminousPreference = [Ominous.Model.OminousPreference]::new()
$global:OminousTypeMappers = [Ominous.Model.TypeMappers]::new()

'mappers.ps1' | % {
    . (Get-Item (Join-Path $PSScriptRoot $_))
}