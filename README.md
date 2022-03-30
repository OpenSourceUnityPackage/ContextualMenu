<h1 align="center" style="border-bottom: none;">Contextual menu📦 </h1>
<h3 align="center">Contextual menu for unity</h3>
<p align="center">
  <a href="https://github.com/semantic-release/semantic-release/actions?query=workflow%3ATest+branch%3Amaster">
    <img alt="Build states" src="https://github.com/semantic-release/semantic-release/workflows/Test/badge.svg">
  </a>
  <a href="https://github.com/semantic-release/semantic-release">
    <img alt="semantic-release: angular" src="https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release">
  </a>
  <a href="LICENSE">
    <img alt="License" src="https://img.shields.io/badge/License-MIT-blue.svg">
  </a>
</p>
<p align="center">
  <a href="package.json">
    <img alt="Version" src="https://img.shields.io/github/package-json/v/OpenSourceUnityPackage/ContextualMenu">
  </a>
  <a href="#LastActivity">
    <img alt="LastActivity" src="https://img.shields.io/github/last-commit/OpenSourceUnityPackage/ContextualMenu">
  </a>
</p>

## What is it ?
Contextual menu is a package that allow you to create button with action in a contextual menu like a RTS.
It is independant and designed to be easy to use as possible.


## Angular typo
```
<type>(<scope>): <short summary>
  │       │             │
  │       │             └─⫸ Summary in present tense. Not capitalized. No period at the end.
  │       │
  │       └─⫸ Commit Scope: animations|bazel|benchpress|common|compiler|compiler-cli|core|
  │                          elements|forms|http|language-service|localize|platform-browser|
  │                          platform-browser-dynamic|platform-server|router|service-worker|
  │                          upgrade|zone.js|packaging|changelog|docs-infra|migrations|ngcc|ve|
  │                          devtools
  │
  └─⫸ Commit Type: build|ci|docs|feat|fix|perf|refactor|test
```

The `<type>` and `<summary>` fields are mandatory, the `(<scope>)` field is optional.


##### Type

Must be one of the following:

* **build**: Changes that affect the build system or external dependencies (example scopes: gulp, broccoli, npm)
* **ci**: Changes to our CI configuration files and scripts (examples: CircleCi, SauceLabs)
* **docs**: Documentation only changes
* **feat**: A new feature
* **fix**: A bug fix
* **perf**: A code change that improves performance
* **refactor**: A code change that neither fixes a bug nor adds a feature
* **test**: Adding missing tests or correcting existing tests
  
For example:
  - feat: create new feature
  - fix: fix an error
  - docs: update readme

For more information [see](https://github.com/angular/angular/blob/master/CONTRIBUTING.md#-commit-message-format)
