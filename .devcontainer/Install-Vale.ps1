$TempPath = [System.IO.Path]::GetTempPath()
Invoke-WebRequest 'https://github.com/errata-ai/vale/releases/download/v2.21.3/vale_2.21.3_Linux_64-bit.tar.gz' -OutFile "$($TempPath)vale.tar.gz"
tar -xvzf "$($TempPath)vale.tar.gz" -C $TempPath
Move-Item "$($TempPath)vale" /bin/vale | Out-Null

chown node:node /workspaces/hugo-theme
