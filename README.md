# Stellarmap
A WinForms based application designed for creating worlds, items, NPCs, and props used by the Dead-Souls MUDlib.

DeadSouls is a LPC-based MUDlib for creating online, multiplayer, text-based adventure games. While it features many amazing things, including a built-in command tool for creating worlds, items, and NPCs through telnet, this often leads to a lot of confusion and slow development for new and experienced developers alike.

Stellarmap is a WinForms based GUI tool for windows that allievates the need to learn programming syntax and function calls or long lists of commands that map directly to such calls by laying out nicely in a standard GUI format. It includes support for defining Domains (worlds), individual rooms within a domain, items, NPCs, weapons, armor, and even supports custom objects through the ability to import XML files defining lists of controls to display and what function calls they would map to.

Dead-Souls primarily defines objects by simply using code syntax to make function calls. As a result, Stellarmap is a code generation system under the hood that disguises function calls, arrays, mappings, and other data structures as simple GUI controls that can easily be understood and edited even by non-programmers. Most importantly, it ignores any and all code that it doesn't need or use and thus will not mangle any files it imports that provde addional functionality or data settings.

You can learn more about the dead-souls MUDLib at http://dead-souls.net/
