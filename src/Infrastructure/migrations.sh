migrationName="$1"

dotnet ef migrations add "$migrationName" -o ./Migrations/ --startup-project ../Api

echo "Migration created ..."

lastTwoMigration=$(ls -t ./Migrations | grep -v 'Designer.cs\|Snapshot' | head -2)

precMigration=$(echo "$lastTwoMigration" | sed -n '2p' | sed 's/\.cs$//')
echo "$precMigration"

if [ $(echo "$lastTwoMigration" | wc -l) -gt 1 ]; then
	newMigration=$(echo "$lastTwoMigration" | sed -n '1p' | sed 's/\.cs$//')
	echo "$newMigration"

	dotnet ef migrations script "$precMigration" -o "./Scripts/$newMigration.sql" --startup-project ../Api
else
	dotnet ef migrations script -o "./Scripts/$precMigration.sql" --startup-project ../Api
fi

echo "SQL File created";

echo "Apply Migration ..."

dotnet dotnet ef database update --startup-project ../Api

echo "Migration Applied"
