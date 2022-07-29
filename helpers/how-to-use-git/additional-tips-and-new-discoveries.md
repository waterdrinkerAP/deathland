# 29.07.2022

We discoverd that git is a bitch when trying to change the case of filenames.

Git's metadata is case insensitive, so `file.txt` is the same as `File.txt`.
Furthermore git has no idea what folders are. Which is why you can't push an
empty folder to Github.
When we want to rename folders or files just to change their case, we must
first change the name to something entirely different.

```
git mv filename temp;
git mv temp FileName
```

Using `git mv` guarantees that the metadata will also be changed correctly.
The `;` symbol should mean "when this command is finished run this command"
on both Linux and Windows.

There is an option called `core.ignoreCase ` which can be set to `false` with:

```
git config --global core.ignoreCase false
```

This may or may not fix the issue. We are not certain at this time.
