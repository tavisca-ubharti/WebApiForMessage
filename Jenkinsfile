pipeline{
    agent { label 'master' }
    environment {
        registry = "ujjwalbharti111/webapi"
        registryCredential = "docker"
    }

    parameters{
        string(
            name: "GIT_SOURCE",
            defaultValue: "https://github.com/tavisca-ubharti/WebApiForMessage.git",
        )
        string(
            name: "Project_Name",
            defaultValue: "API"
        )
        string(
            name: "SOLUTION_PATH",
            defaultValue: "WebApiForHelloHii.sln",
        )
        string(
            name: "DOTNETCORE_VERSION",
            defaultValue: "2.2",
        )
        string(
            name: "TEST_SOLUTION_PATH",
            defaultValue: "WebApiForHelloHii.Tests/WebApiForHelloHii.Tests.csproj",
        )
        string(
            name: "ENV_NAME",
            defaultValue: "API",
        )
        string(
            name: "SOLUTION_DLL_FILE",
            defaultValue: "WebApiForHelloHii.dll",
        )
        string(
            name: "DOCKER_USER_NAME",
            description: "Enter Docker hub Username"
        )
        string(
            name: "DOCKER_PASSWORD",
            description:  "Enter Docker hub Password"
        )
        choice(
            name: "RELEASE_ENVIRONMENT",
            choices: ["Build","Test", "Publish","Deploy"],
        )
    }
    stages{
        stage('Build'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Build" || params.RELEASE_ENVIRONMENT == "Test" || params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps{
                powershell '''
                    dotnet restore ${SOLUTION_PATH} --source https://api.nuget.org/v3/index.json
                    dotnet build ${PPOJECT_PATH} 
                    dotnet test ${TEST_SOLUTION_PATH}
                '''
            }
        }
        stage('Publish'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps{
                powershell '''
                    dotnet publish ${PROJECT_PATH}
                '''
            }
        }
        stage ('Deploy') {
            when{
                expression{params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps {
                writeFile file: 'WebApiForHelloHii/bin/Debug/netcoreapp2.2/publish/Dockerfile', text: '''
                        FROM mcr.microsoft.com/dotnet/core/aspnet\n
                        ENV NAME ${Project_Name}\n
                        CMD ["dotnet", "${SOLUTION_DLL_FILE}"]\n'''
                
                powershell "docker build WebApiForHelloHii/bin/Debug/netcoreapp2.2/publish/ --tag=${Project_Name}:${BUILD_NUMBER}"    
                powershell "docker tag ${Project_Name}:${BUILD_NUMBER} ${DOCKER_USER_NAME}/${Project_Name}:${BUILD_NUMBER}"
                powershell "docker push ${DOCKER_USER_NAME}/${Project_Name}:${BUILD_NUMBER}"
            }
        }
    }
    post{
        always{
            deleteDir()
       }
    }
}
