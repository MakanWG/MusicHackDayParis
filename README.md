MusicHackDayParis
=================

This Project was developped at the Music Hack Day in Paris.

The code is pretty ugly in some parts but here what it does.

Fetch a given soundcloud User playlists.
You can then pick a song in the list.
The song is then passed to the Echonest API that will return a full analysis of the song.
This analysis is then processed again by an algorithm I wrote that will recognize very similar parts in the song.
The song will then play and once it encounters one of those part, it will jump to the similar part.
The aim is to make that inaudible.
There are some inconsistencies with the timers so we often hear the jump. 
But I'm close !
