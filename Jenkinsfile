pipeline{
    agent any
   
    parameters{
        string(
            name: "GIT_SOURCE",
            defaultValue: "https://github.com/tavisca-ubharti/WebApiForMessage.git",
        )
        string(
            name: "Project_Name",
            defaultValue: "webapi"
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
            defaultValue: "api",
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
                expression{params.RELEASE_ENVIRONMENT == "Deploy" || params.RELEASE_ENVIRONMENT == "Build" || params.RELEASE_ENVIRONMENT == "Test" || params.RELEASE_ENVIRONMENT == "Publish"}
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
                expression{params.RELEASE_ENVIRONMENT == "Deploy"|| params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps{
                powershell '''
                    dotnet publish ${PROJECT_PATH}
                '''
            }
        }
        stage('SonarQube Static Code Analysis'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Deploy"|| params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps{
                powershell '''
                    dotnet C:/Users/ubharti/sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0/SonarScanner.MSBuild.dll begin /k:"WebApi" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="0b1e85ae8473a15f0f70e46ad157199d489453ca"
                    dotnet build
                    dotnet test
                    dotnet C:/Users/ubharti/sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0/SonarScanner.MSBuild.dll end /d:sonar.login="0b1e85ae8473a15f0f70e46ad157199d489453ca"
                '''
            }
        }
        stage ('Deploy') {
            when{
                expression{params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps {
                              
                powershell "Copy-Item WebApiForHelloHii/bin/Debug/netcoreapp2.2/publish/* docker/ -Recurse"
                powershell "docker build docker/ --tag=${Project_Name}"    
                powershell "docker tag ${Project_Name} ${DOCKER_USER_NAME}/${Project_Name}"
                powershell "docker login -u=${DOCKER_USER_NAME} -p=${DOCKER_PASSWORD}"
                powershell "docker push ${DOCKER_USER_NAME}/${Project_Name}"
                powershell "docker run -p 8112:80 ${DOCKER_USER_NAME}/${Project_Name}"
            }
        }
    }
    post{
        always{
            deleteDir()
       }
    }
}
