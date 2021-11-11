nuget引用docker打包编译

docker build -t 13871772983/atomiccore.webservice.requestagent .

本地项目引用docker打包编译

docker build -f "D:\GitHub_Pros\AtomicCore\AtomicCore.WebService.RequestAgent\Dockerfile" --force-rm -t 13871772983/atomiccore.webservice.requestagent  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=AtomicCore.WebService.RequestAgent" "D:\GitHub_Pros\AtomicCore" 

镜像推送

docker push 13871772983/atomiccore.webservice.requestagent

镜像容器化运行

docker run -d -p 8778:80 --name=atomiccore.webservice.requestagent  13871772983/atomiccore.webservice.requestagent


CMD命令项目编译

dotnet build --configuration Release "AtomicCore.WebService.RequestAgent.csproj"