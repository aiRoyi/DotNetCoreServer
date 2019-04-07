$protoTool = ".\"
$baseDir = ".\"
$baseOutDir = ".\Output\"

if ($args.Count -ge 1)
{
	$protoTool = $args[0]
}
if ($args.Count -ge 2)
{
	$baseDir = $args[1]
}
if ($args.Count -ge 3)
{
	$baseOutDir = $args[2]
}

Write-Host $args


$modules = @(".", "Common", "C2S", "S2C")

for ($i = 0; $i -lt $modules.Count; $i++)
{
	$fullDir = $baseDir + $modules[$i] + "\"
	$files = Get-ChildItem $fullDir
	$csharpOutDir = $baseOutDir + $modules[$i] + "\"
	If(!(test-path $csharpOutDir))
	{
		New-Item -ItemType Directory -Force -Path $csharpOutDir
	}
	$commandOption = "--proto_path=" + $baseDir + " --csharp_out=" + $csharpOutDir
	for ($j=0; $j -lt $files.Count; $j++)
	{
		$extn = [IO.Path]::GetExtension($files[$j])
		if ($extn -eq ".proto" )
		{
			$command = $protoTool + " " + $commandOption + " " + $fullDir + $files[$j]
			Write-Host $command
			iex "& $command"
		}
	}
}