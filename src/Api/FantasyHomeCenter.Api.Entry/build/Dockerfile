FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV TZ=Asia/Shanghai
#��֤���õ�������
RUN sed -i "s@http://deb.debian.org@http://mirrors.aliyun.com@g" /etc/apt/sources.list
RUN apt-get update -y && apt-get install -y fontconfig && apt-get clean
COPY /Fonts /usr/share/fonts
RUN fc-cache
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "FantasyHomeCenter.Entry.dll"]
