# Git Basics

Type: #Idea 
Topic: [[Git]] | [[Programming]]

It is better to create our repositories on GitHub's website and then clone them to our 
local machine with:

```bash
git clone <SSH URL>
```

If we want to initialize an empty local repository - `cd` into the desire folder and type:

```bash
git init
```

When we  `cd` into the repository folder we can check it's origin by typing:

```bash
git remote -v
```

Once we put files in that repository and start making changes to them, we can check
the status of the repo with:

```bash
git status
```

`git status -s` gives shorter less detailed output that follows the form:

```bash
 M README  # M stands for modified file
MM Rakefile  # Left M is status of staging area, right is working tree
A  lib/git.rb # A stands for added to staging area
?? LICENSE.txt # ?? is a new file that is not tracked
```

Mark new and recently changed files for a commit by adding them to the staging area:

```bash
git add <file_name>
```

We don't need to add all the changes made to a file to the staging area. We can go over the
chunks of changes and chose what to add with:

```bash
git add -p <file_name>
```

Remove file from the staging area with:

```bash
git restore --staged <file_name>
```

Revert changes back to main with:

```bash
git restore <file_name>
```

When adding or restoring, we can replace the file name with `.`, to apply the 
command to all files in the folder.

See exactly what was changed with one of:

```bash
git diff  # This shows only unstaged changes
git diff --staged 
```

Finalize changes by committing all files in the staging area, and adding a message
for the new commit. Use imperative mood in messages and don't end them with a
punctuation mark. First line of the message should be no longer than 50 characters.
Check [[Git Commit Message Rules]] for more information.

```bash
git commit -m "One line message here"
```

To set VS Code as the editor for commit messages type:

```bash
git config --global core.editor "code --wait"
```

Skip the staging are and add or remove all files known to git by using:

```bash
git commit -a
```

To make changes to last commit use:

```bash
git commit --amend
```

The current staging area is used to replace last commit. If no changes were made,
only the commit message will be changed.

Give commits easy to remember name with tags:

```bash
git tag -a <version_name> -m "version message" # create tag
git tag -l # list tags
git show <version_name> # see tag and commit information
```

Tags are not transferred to the remote server by default. Use:

```bash
git push --tags
```

See a list of last 2 commits and the changes they introduced with:

```bash
git log -p -2
```

To get a single line summary of each commit use one of:

```bash
git log --oneline
git log --pretty=oneline
```

For more information on `--pretty` and its values check the [book][1].

When removing or renaming files it is faster to use git commands:

```bash
git rm  # Rmove
git mv  # Move/Rename
```

This will skip having to add the file to the staging area. However if the files is
already in the staging area, add `-f` to force the change.

To go back in time one commit, use:

```bash
git reset main^
```

For moving back more than one commit, type:

```bash
git reset main~<number_of_steps>
```

Git on Windows will convert line endings in files from `LF` to `CRLF`, when committing.
To prevent this behavior and make sure that all files we push from a Windows machine 
end their lines with `LF` use:

```bash
git config --global core.eol lf
```

To make sure that all files we pull to a Linux machine end their lines with `LF` use:

```bash
git config --global core.autocrlf input
```

To upload a repository to a remote server use:

```bash
git push
```

Check for changes on the remote server with:

```bash
git fetch
```

Pull files from and merge with the remote repository using:

```bash
git pull
```

To get a list of remotes use:

```bash
git remote
```

To get more information for a particular remote use:

```bash
git remote show <name>
```

To rename or remove a remote connection use one of:

```bash
git remote rename <name>
git remote remove <name>
```

To add a remote use:

```bash
git remote add <name> <URL>
```

[1]:https://git-scm.com/book/en/v2/Git-Basics-Viewing-the-Commit-History