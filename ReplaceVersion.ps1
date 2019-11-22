function getVersion()
{
    $b = '1.0.200.00'
    Write-Host "Version found: $b"
    return $b
}

function SetVersion ($version)
{

        [System.Object[]]$filesToExecute = Get-ChildItem $PSScriptRoot -Filter "AssemblyInfo.cs" -Recurse

        Write-Host " Replace 'Assembly[.*]Version' to the version ($version) in 'AssemblyInfo.cs' : $($filesToExecute.Length) files"
        
        if($filesToExecute.Count -gt 0)
        {
            $filesToExecute |
            Foreach-Object{
                $fullName = $_.FullName

                #if ($fullName -notmatch '\\Shell\\Properties\\') {
                if ($fullName -notmatch '\\publish\\') {
                    $regexp = 'Version\(\"[\d\.]+\"\)';
                    $repstr = 'Version("' + $version + '")';

                    ## Execute Replace RegExp to file content
                    $fileTextIn = Get-Content $fullName
                    $fileTextOut = % { $fileTextIn -Replace $regexp, $repstr }

                    if([string]::Join("|",$fileTextIn) -ne [string]::Join("|",$fileTextOut))
                    {
                        Out-File -filePath $fullName -Encoding "UTF8" -inputobject $fileTextOut
                        $filesModified++;
                    }
                }
            }
        }
        Write-Host "   total AssemblyInfo.cs replaced: $filesModified";
}

# First get tag from Git
$version = getVersion
Setversion $version