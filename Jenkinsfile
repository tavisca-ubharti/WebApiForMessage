pipeline {
    agent any
    parameters {
        string(defaultValue: "https://github.com/tavisca-ubharti/WebApiForMessage.git", description: '', name: 'GIT_SSH_PATH')
        string(defaultValue: "WebApiForHelloHii.sln", description: '', name: 'SOLUTION_FILE_PATH')
        string(defaultValue: "WebApiForHelloHii.Tests/WebApiForHelloHii.Tests.csproj", description: '', name: 'TEST_PROJECT_PATH')
    }
    stages {
        stage('Build') {
            steps {
                sh 'dotnet" restore ${SOLUTION_FILE_PATH} --source https://api.nuget.org/v3/index.json'
                sh 'dotnet build  ${SOLUTION_FILE_PATH} -p:Configuration=release -v:n'
            }
        }
        stage('Test') {
            steps {
                sh 'dotnet test ${TEST_PROJECT_PATH}'
            }
        }
    }
}