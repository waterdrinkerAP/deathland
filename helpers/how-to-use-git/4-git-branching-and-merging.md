# Git Branching and Merging

Type: #Idea 
Topic: [[Git]] | [[Programming]]

Every commit in Git contains data about itself (size, author, committer, message) and two
pointers. A pointer to a snapshot of the content that was staged at the time of the commit
and a pointer to the previous commit. Branches are just named pointers to a commit.
When we first create a repository there is only one branch called `main` that points to the
latest commit.  We can create a new branch by using:

```bash
git branch <name_of_branch>
```

```
　　　　　　　　　　　　　　　　　　HEAD
                                ↓
                  new_branch  main
                           ↘   ↙
commit 1 ←-- commit 2 ←-- commit 3
  ↓            ↓            ↓
snapshot A ← snapshot B ← snapshot C
```

Another pointer called `HEAD` points to the currently active branch. To move the head use:

```bash
git checkout <destination> # use on branches and commits
git switch <destination> # use only on branches
```

List available branches, marking current one, with:

```bash
git branch
```

Create a branch and move `HEAD` at the same time with one of these commands:

```bash
git checkout -b <name_of_branch>
git switch -c <name_of_branch>
```

Move `HEAD` to the previously selected branch with one of:

```bash
git checkout -
git switch -
```

See information about all branches and how they diverge with:

```bash
git log --graph --all
```


`HEAD` can point to a branch or to a specific commit. When `HEAD` is not pointing to a branch
we are in a state of "detached HEAD". When detaching the head we need to use the
`checkout` command, the `switch` command works only with branches. To make `HEAD`
point to a specific commit we need the first 7 characters of a commit's SHA. For example:

```bash
git checkout 9b8eb90
```

To push a new branch to a remote Git repository first `checkout` the branch and then use:

```bash
git push -u origin <name_of_branch>
```

To merge two branches first `checkout` the branch you want to merge into and then call the
`merge` command:

```bash
git checkout main
git merge new_branch
```

If the commit at `new_branch` is a direct descendent of the one `main` is pointing at, then 
`main` will simply be fast-forwarded to point to the new commit.

```
                       main     new_branch
                        ↓          ↓
commit 1 ← commit 2 ← commit 3 ← commit 4
```

If the two branches have diverged at a previous point, Git will still try to merge them.

```
                       main   
                        ↓
commit 1 ← commit 2 ← commit 4
                    ↖  
                      commit 3
                        ↑
                     new_branch
```

Merges of this type create a new "merge commit" with two parents and move the pointer
of the checked out branch to this new commit.

```
                                  main   
                                   ↓
commit 1 ← commit 2 ← commit 4 ← commit 5
                    ↖          ↙
                      commit 3
                        ↑
                     new_branch
```

However if the two branches introduce different changes to the same files, there might
be conflicts. After Git detects the conflicts, all we have to do is open the files in any editor
and chose how to deal with them.

Changes will look like this:

```
<<<<<<< HEAD
Changes from the currently checked out branch.
=======
Changes from the branch we are trying to merge into the current one.
>>>>>>> new_branch
```

After we handle the conflicts, we need to `add` the files to the staging area and `commit`.
It is a good idea to check the `status` of the repository often. When a `merge` has failed
due to conflicts and we call `status`, Git tells us that we are in the middle of a merge.
If we don't want to deal with the conflicts, we can undo the merge with:

```bash
git merge --abort
```

We can see a history of how our branches have diverged and merged with:

```bash
git log --oneline --graph --all
```

A sample output from that command would be:

```
*   ff216e6 (HEAD -> main, origin/main, origin/HEAD) Merge branch 'office'
|\
| * 2f164a5 (origin/office, office) Edit hello_world.txt from Office
* |   f8a02cf Merge branch 'test'
|\ \
| * | a952772 (test) Edit hello_world.txt to test branching
| |/
* / 856350d Add a question to hello_world.txt
|/
* b77c90c Edit hello_world.txt from Linux
* 9b8eb90 Edit README.md
* 79b53ee Edit README.md and hello_world.txt
```

Each `*` on the graph represents a commit.
Branch names and remote origins are shown in parentheses.

To see the last commit on each branch run:

```bash
git branch -v
```

To see branches that are already merged into the current one type:

```bash
git branch --merged
```

To delete a branch that has been merged and is no longer needed first make sure it's not
checked out, then do:

```bash
git branch -d <name_of_branch>
```

Git prevents deletion of branches that are not fully merged in. To force the deletion use:

```bash
git branch -D <name_of_branch>
```

It is not a good idea to rename branches that were pushed to remote.
To rename a local branch and push it use:

```bash
git branch --move <old_branch_name> <new_branch_name>
git push --set-upstream origin <new_branch_name>
```

To delete the old branch from remote use:

```bash
git push origin --delete <old_branch_name>
```

Local and remote branches can differ in many ways. Git keeps a reference to both local and
remote branches. To see a list of all branches use:

```bash
git branch --all
```

To checkout a branch that exists only on remote but not locally use one of the following:

```bash
git checkout -b <name_of_local_branch> origin/<name_of_remote_branch>
git checkout --track origin/<name_of_remote_branch>
git checkout <name_of_remote_branch> # must be identical with the remote branch name
```

The first option allows us to have a different name for our local version of the branch.

To see a list of branches with their remotes and their last commits type:

```bash
git branch -vv
```

From the output of this command you can also see what local branches don't have a remote
counterpart and by how many commits local and remote branches differ.
Information about remote branches is stored locally and can be out of date. To update use:

```bash
git fetch --all
```

What the `git pull` command actually does is, it fetches all changes from the server and
then tries to merge the remote branch into the local branch of the same name that you have
currently checked out.

Apart from merging we can also rebase branches into other branches. Rebasing allows for
the commit history to look as a straight line. To rebase a branch use  one of the following:

```bash
git rebase main # if you are currently on new_branch
git rebase main new_branch # from any branch
```

This creates a copy of the changes introduced by the `new_branch` and puts them in front
of the last commit of the `main` branch:

```
                       main     new_branch
                        ↓          ↓
commit 1 ← commit 2 ← commit 4 ← commit 3`
                    ↖  
                      commit 3
```

We can then continue the merge with the following two steps:

```bash
git checkout main
git merge new_branch
```

If successful the result will look like this:

```
                                  main
                                   ↓
commit 1 ← commit 2 ← commit 4 ← commit 3`
                    ↖              ↑
                      commit 3  new_branch
```

It is best to use rebasing only on local repositories. Rebasing repositories that other people
are working on can create a lot of confusion and may even make the repository unusable.

For strategies on how to use branching and merging see :
https://youtu.be/Uszj_k0DGsg?t=488