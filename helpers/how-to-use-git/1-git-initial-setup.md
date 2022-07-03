# Git Initial Setup

Type: #Idea 
Topic: [[Git]] | [[Programming]]

To set up Git for use with GitHub we first need to configure the following settings:

```bash
git config --global user.name "Your name"
```

```bash
git config --global user.email "Your@email.com"
```

```bash
git config --global init.defaultBranch main
```

```bash
git config --global color.ui auto
```

We can check what we have done with:

```bash
git config --get user.name
```

```bash
git config --get user.email
```

```bash
git config --list --show-origin
```

To easily push code to GitHub we need to generate an [[SSH]] key:

```bash
ssh-keygen -t ed25519 -C <youremail>
```

In our GitHub account's `Settings > SSH and GPG keys` add the `New SSH Key`:

```bash
cat ~/.ssh/id_ed25519.pub
```

To test if we have successfully linked Git to our GitHub:

```bash
ssh -T git@github.com
```

If the following is shown, just type `yes`. We can check the [fingerprint][1] if we like.

```
The authenticity of host 'github.com (IP ADDRESS)' can't be established.
RSA key fingerprint is SHA256:nThbg6kXUpJWGl7E1IGOCspRomTxdCARLviKw6E5SY8.
Are you sure you want to continue connecting (yes/no)?
```

To make sure that all files we pull to a Linux machine end their lines with `LF` use:

```bash
git config --global core.autocrlf input
```

To make sure files we push from a Windows machine end their lines with `LF` use:

```bash
git config --global core.eol lf
```


To set VS Code as our editor for commit messages type:

```bash
git config --global core.editor "code --wait"
```

For additional information check:

```bash
git help
```

``` bash
man git
```

Or: https://git-scm.com/book/en/v2

To set up tab-completion in PowerShell see:
https://git-scm.com/book/en/v2/Appendix-A%3A-Git-in-Other-Environments-Git-in-PowerShell


[1]: https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/githubs-ssh-key-fingerprints