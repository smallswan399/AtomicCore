nuget引用docker打包编译
docker build -t 13871772983/atomiccore.iostorage.storageport .


直接项目应用后他的docker打包编译

docker build -f "D:\GitHub_Pros\AtomicCore\AtomicCore.IOStorage.StoragePort\Dockerfile" --force-rm -t 13871772983/atomiccore.iostorage.storageport  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=AtomicCore.IOStorage.StoragePort" "D:\GitHub_Pros\AtomicCore"


镜像推送至hub.docker.com

docker push 13871772983/atomiccore.iostorage.storageport

外挂存储容器启动

docker pull alpine
docker pull 13871772983/atomiccore.iostorage.storageport

docker run --name alpine-netcore-uploads -it -v uploads:/app/wwwroot/uploads alpine sh

docker run -d -p 8777:80 --name=atomiccore.iostorage.storageport -it --volumes-from alpine-netcore-uploads 13871772983/atomiccore.iostorage.storageport