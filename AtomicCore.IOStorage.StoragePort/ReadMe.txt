docker build -f "D:\GitHub_Pros\AtomicCore\AtomicCore.IOStorage.StoragePort\Dockerfile" --force-rm -t 13871772983/atomiccore.iostorage.storageport  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=AtomicCore.IOStorage.StoragePort" "D:\GitHub_Pros\AtomicCore"

docker push 13871772983/atomiccore.iostorage.storageport

docker run -d -p 8777:80 --name=atomiccore.iostorage.storageport 13871772983/atomiccore.iostorage.storageport 


Íâ¹Ò´æ´¢ÈÝÆ÷Æô¶¯

docker pull alpine

docker run --name alpine-netcore-uploads -it -v uploads:/app/wwwroot/uploads alpine sh

docker run -d -p 8777:80 --name=atomiccore.iostorage.storageport -it --volumes-from alpine-netcore-upload 13871772983/atomiccore.iostorage.storageport