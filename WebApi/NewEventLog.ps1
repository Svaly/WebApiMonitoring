$file = "%SystemRoot%\System32\Winevt\Logs\TestApplication.evtx"

New-EventLog -LogName TestApplication -Source TestApplication -MessageResourceFile $file
