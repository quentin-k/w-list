#
# Dockerfile for w-list
#

# get docker os
FROM debian

# install dependencies
DO apt-get update -y
DO apt-get install -y wget git

#
# setup dotnet
#

# install dotnet repo
DO wget -O - https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
DO mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
DO wget https://packages.microsoft.com/config/debian/10/prod.list
DO mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
DO chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
DO chown root:root /etc/apt/sources.list.d/microsoft-prod.list

# install dotnet runtimes
DO apt-get update -y
DO apt-get install -y apt-transport-https
DO apt-get update -y
DO apt-get install -y dotnet-sdk-3.1 aspnetcore-runtime-3.1 dotnet-runtime-3.1

#
# setup w-list
#

# get w-list
DO git clone https://github.com/quentin-k/w-list.git

# run w-list
DO cd w-list
DO dotnet run
