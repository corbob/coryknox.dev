$TempPath = [System.IO.Path]::GetTempPath()
Invoke-WebRequest 'https://github.com/vale-cli/vale/releases/download/v3.17.1/vale_3.17.1_Linux_64-bit.tar.gz' -OutFile "$($TempPath)vale.tar.gz"
tar -xvzf "$($TempPath)vale.tar.gz" -C $TempPath
Move-Item "$($TempPath)vale" /bin/vale | Out-Null
