# Setup Guide

1. Clone repo or download zip
2. Open VS Solution found in the following directory - \umbraco-8-total-coding-main\Automated-Umbraco\Automated-Umbraco.sln
3. IMPORTANT: Make sure to Restore Nuget packages as the Umbraco dependencies have not been committed - Open VS >> Right Click on Solution >> Restore Nuget Packages
4. Run a Clean Solution build
5. Run solution
    - Possible issue: If you run the application via VS, if you get an exception saying "cant find file \bin\roslyn\csc.exe", this will be because the dependencies havent been downloaded/built. Make sure to Restore Nuget Packages, Rebuild the solution and try again  
    - Possible issue: if running via VS, during the Umbraco Installation Process, I occassionally had a "Managed Deugging Assistant 'LoaderLock'" - dont worry, just click Continue. This may happen about 3 times, it doesnt cause any issues, just make sure to click Continue so the debugger doesnt hold up the installation process. This only happens during installation process and I believe is a known issue
