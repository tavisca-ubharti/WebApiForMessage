pipeline {
    agent any
    parameters {
        string(name: 'GIT_HTTPS_PATH', defaultValue: 'https://github.com/tavisca-ubharti/WebApiForMessage.git')
        string(name: 'TEST_PROJECT_PATH', defaultValue: 'WebApiForHelloHii.Tests/WebApiForHelloHii.Tests.csproj')
        string(name: 'API_SOLUTION', defaultValue: 'WebApiForHelloHii.sln')
        choice(name:'choices',choices: ['Both','Build', 'Test'])
    }
    stages {
        stage('restore'){
            steps{
                powershell'''
                    dotnet restore ${API_SOLUTION} --source https://api.nuget.org/v3/index.json
                '''
            }
        }
        stage('Build') {
            steps {
               powershell '''             
               dotnet build ${API_SOLUTION} -p:Configration=release -v:q
               '''
             
            }
        }
        stage('Test') {
            steps {
              powershell'''
              dotnet test ${TEST_PROJECT_PATH}
              '''
            }
        }
        stage('Publish') {
            steps {
                powershell '''               
                dotnet publish ${API_SOLUTION} -c Release                              
                '''
            }
        }
       
         stage('Compress') {
            steps {
                powershell '''
                compress-archive WebApiForHelloHii\\bin\\Release\\netcoreapp2.2\\publish\\* artifactFiles.zip -Update           
                '''
            }
        }
       
        stage('Deploy') {
            steps {
                powershell '''               
                expand-archive artifactFiles.zip ./ -Force
                dotnet WebApiForHelloHii.dll               
                '''
            }
        }
    }
}
