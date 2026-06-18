# LocalDB Scratchpad

This folder is used by the MSTest setup as a LocalDB scratchpad.

Test initialization may set `AppDomain.CurrentDomain.DataDirectory` to this folder and create or attach transient `.mdf` and `.ldf` files here. These database files are intentionally ignored by git and must not be committed.

The README exists to document the folder and keep it present in source control.
