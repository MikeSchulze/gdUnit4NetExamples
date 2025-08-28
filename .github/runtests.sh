#!/bin/bash

# Cleanup old logs
# find . -type d -name ".godot" -exec rm -rf {} +
# find . -type d -name "gdunit4_testadapter_v5" -exec rm -rf {} +
# rm -rf TestResults
# dotnet build

total_projects=0
passed_projects=0
failed_projects=0
passed_projects_list=()
failed_projects_list=()

for project in $(find . -name "*Test*.csproj" -o -name "*.Test.csproj"); do
    project_name=$(basename "$project" .csproj)
    project_dir=$(dirname "$project")
    total_projects=$((total_projects + 1))

    echo "========================================="
    echo "[$total_projects] Running tests for: $project_name"
    echo "Project path: $project"
    echo "========================================="

    # Run the test and capture the exit code
    dotnet test "$project" --no-build --settings .runsettings-ci --verbosity normal \
        --logger:"html;LogFileName=test-result-$project_name.html" \
        --logger:"trx;LogFileName=test-result-$project_name.trx"

    exit_code=$?

    if [ $exit_code -eq 0 ]; then
        echo "✅ SUCCESS: $project_name completed successfully"
        passed_projects=$((passed_projects + 1))
        formatted_row=$(printf "%-6s %-30s %-12s %-60s" "✅ PASS" "$project_name" "$exit_code" "$project")
        passed_projects_list+=("$formatted_row")
    else
        echo "❌ FAILED: $project_name failed with exit code $exit_code"
        failed_projects=$((failed_projects + 1))
        formatted_row=$(printf "%-6s %-30s %-12s %-60s" "❌ FAIL" "$project_name" "$exit_code" "$project")
        failed_projects_list+=("$formatted_row")
    fi

    echo ""
done

echo "========================================="
echo "SUMMARY:"
echo "Total projects: $total_projects"
echo "Passed: $passed_projects"
echo "Failed: $failed_projects"
echo ""

printf "%-6s %-30s %-12s %-60s\n" "Status" "Project Name" "Exit Code" "Project Path"
printf "%-6s %-30s %-12s %-60s\n" "------" "------------------------------" "------------" "------------------------------------------------------------"
for passed_row in "${passed_projects_list[@]}"; do
    echo "$passed_row"
done

# Print failed projects
for failed_row in "${failed_projects_list[@]}"; do
    echo "$failed_row"
done

echo "========================================="

# Exit with error if any tests failed
if [ $failed_projects -gt 0 ]; then
    exit 1
fi
