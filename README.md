# DEVELOPERS README #

dasjhdkajsh

This README section identifies developer notes for developing this game. Please refer to this if you get stuck anywhere.

## BIT BUCKET ##
#### COMMITS AND PUSH #####
The **Scene conflicts** will always be there if multiple developers are working on the same scene.  
**Solution 1:** Please create prefabs as much as possible or else data will be lost (depending on what you choose to resolve conflicts).  
**Solution 2:** Make sure you modify scene stuff after you rebase / merge your local copy just before committing and pushing to master (if possible).  
**Solution 3:** Creating two pushes to master. (If nothing works)  
- Push all your changes except scene file to master branch.  
- Rebase / Merge your local copy, fix/add scene changes.  
- Push scene file to master branch. (This will be notified as 'Merge with conflicts')  
**Solution 4:** Create a copy of the scene and make your changes there and then copy stuff from the new scene to original scene after you have rebased your local copy from the repository.

#### MARKDOWNS FOR THIS README #####
Please use the proper markdown guidelines for adding anything to this readme to make it easy for readers.  
To learn about markdowns please visit the [Markdowns Demo](https://bitbucket.org/tutorials/markdowndemo) section by Bitbucket.  
Minor Tip: For newlines add two space characters at the end of each line.

***

# INSTALL README (Placeholder)#

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact
