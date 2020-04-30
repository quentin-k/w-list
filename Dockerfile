#
# Dockerfile for w-list
#

# get docker os
FROM debian

# install dependencies
RUN apt-get update -y
RUN apt-get install -y wget git

#
# setup dotnet
#

# install dotnet repo
RUN wget -O - https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
RUN mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
RUN wget https://packages.microsoft.com/config/debian/10/prod.list
RUN mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
RUN chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
RUN chown root:root /etc/apt/sources.list.d/microsoft-prod.list

# install dotnet runtimes
RUN apt-get update -y
RUN apt-get install -y apt-transport-https
RUN apt-get update -y
RUN apt-get install -y dotnet-sdk-3.1 aspnetcore-runtime-3.1 dotnet-runtime-3.1

#
# setup w-list
#

# get w-list
RUN git clone https://github.com/quentin-k/w-list.git

# run w-list
RUN cd w-list
RUN dotnet run
