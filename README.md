# w-list
A social program for seeing how communication develops in a severly restrained environment. The name comes from the shortening of wordlist and whitelist, due to the program utilizing a wordlist that is a whitelist for allowable communication.
# technolgies
|Product|Reason|
|---|---|
SQLite|Storing information
# how to use

### linux

If you wish to use w-list on your network run `ip addr | grep inet` to get your ip address. Then run the command `sudo EDITOR /etc/hosts` where EDITOR is your preferred text editor, then add `IPADDR domainaddr` where IPADDR is the ip address you found. In my case I added the line `10.0.0.24 domainaddr` to my `/etc/hosts`.

If you do not wish to use w-list on your network run the command `sudo EDITOR /etc/hosts` where EDITOR is your preferred text editor and append `domainaddr` to the line that has `127.0.0.1`.

### windows

If you wish to use w-list on your network run `ip config` to get your ip address. Then run your favorite text editor as administrator and open `C:\Windows\System32\Drivers\etc\hosts` then add the line `IPADDR domainaddr` where IPADDR is the ip address you found to the bottom of the file.

If you do not wish to use w-list on your network run your favorite text editor as administrator and open `C:\Windows\System32\Drivers\etc\hosts` then add the line `127.0.0.1 domainaddr` to the bottom of the file.