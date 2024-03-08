$migrationName=$args[0]

dotnet ef migrations add $migrationName -o .\Migrations\ --startup-project .\..\Api

echo "Migration created ..."

$lastTwoMigration = gci .\Migrations -Exclude *.Designer.cs* ,*Snapshot* | sort LastWriteTime | select -last 2

$precMigration = $lastTwoMigration[0].BaseName
echo $precMigration
if($lastTwoMigration.Count -gt 1){
	$newMigration = $lastTwoMigration[1].BaseName
	echo $newMigration

	dotnet ef migrations script $precMigration  -o .\Scripts\$newMigration.sql --startup-project .\..\Api
}
else{
	dotnet ef migrations script -o .\Scripts\$precMigration.sql --startup-project .\..\Api
}
echo "SQL File created";