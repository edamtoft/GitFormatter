# GitFormatter
Git-Compatible Versioning for .NET

## Background
Git is pretty awesome. If you're like me, you probably wish that you had version control in every part of your life. An annotated, immutable, and verifiable history is an extremely powerful tool. Unfortunately, in the age of web-based apps, a lot of software misses the mark here. Finding a clear change history, much less one you can diff, branch from, and merge to, is rare. Sometimes, you might have a preview/publish feature with revision history, but in a typical database this is often a [comlpex mash of tables](https://www.mediawiki.org/wiki/Manual:Revision_table). This makes version control hard, non-standardized, and opt-in.


The first crux of solving this problem is the non-standardized part. Git establishes the gold standard for version control, but it caters to file-based on-disk operations. [Git's internals](https://git-scm.com/book/en/v1/Git-Internals), however, provide an simple, standard way of modeling abstract versionable data. This tool aims to make it easy to make git-compatible commits, blobs, trees, and hashes in .NET applications with the endgame of making data versioning for apps easy, standardized, git-compatible, and pluggable with any database backend. 

# Usage
Work in progress. See test cases for more info.
