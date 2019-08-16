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
                    dotnet restore $ENV:WORKSPACE\\$($env:API_SOLUTION) --source https://api.nuget.org/v3/index.json
                '''
            }
        }
        stage('Build') {
            steps {
               powershell '''             
               dotnet build $ENV:WORKSPACE\\$($env:API_SOLUTION) -p:Configration=release -v:q
               '''
             
            }
        }
        stage('Test') {
            steps {
              powershell'''
              dotnet test $ENV:WORKSPACE\\$($env:TEST_PROJECT_PATH)
              '''
            }
        }
        stage('Publish') {
            steps {
                powershell '''               
                dotnet publish $ENV:WORKSPACE\\$($env:API_SOLUTION) -c Release                              
                '''
            }
        }
       
         stage('Compress') {
            steps {
                powershell '''
                compress-archive WebAppDemo\\bin\\Release\\netcoreapp2.2\\publish\\* artifactFiles.zip -Update           
                '''
            }
        }
       
        stage('Deploy') {
            steps {
                powershell '''               
                expand-archive artifactFiles.zip C:\\Users\\ubharti\\Desktop\\unzip -Force
                dotnet WebAppDemo.dll               
                '''
            }
        }
    }
}
