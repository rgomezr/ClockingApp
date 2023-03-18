![.NET_Build](https://img.shields.io/github/actions/workflow/status/rgomezr/ClockingApp/dotnet.yml?label=.NET%20Build)
![release_version](https://img.shields.io/github/v/release/rgomezr/ClockingApp?include_prereleases)
![latest_commit](https://img.shields.io/github/last-commit/rgomezr/ClockingApp)
![closed_prs](https://img.shields.io/github/issues-pr-closed-raw/rgomezr/ClockingApp)
![closed_issues](https://img.shields.io/github/issues-closed-raw/rgomezr/ClockingApp)
![code_size](https://img.shields.io/github/languages/code-size/rgomezr/ClockingApp)
![stars](https://img.shields.io/github/stars/rgomezr/ClockingApp)

# 🕦 ClockingApp

## 🎯 Objective
Built to ease the clocking experience when WFH

### ⏺️ Current Functionality

* With current version you can:
	* Start & Finish your working period for current date.
	* Start & Finish resting/break periods within working period.
	* View your weekly clockings for a specified date with the following summarised information:
		* Weekly working days.
		* Weekly working hours.
		* Weekly Overtime hours (if overtime feature is enabled)
		* Daily basic info (Start, End & Duration for Work and Breaks)
		* Daily breakdown for resting periods.
	* Have an invoice view for your weekly clockings.
		* Time Zone for invoice can be chosen between:
			* GMT + 0
			* GMT + 1
	* App settings can be modified within `appsettings.json`:
		* Specify overtime threshold hours.
		* Specify default working hours per week day.
		* Specify Paid Break Time (if applies)
	* Startup Custom Config:
		* Mongo client is defined as a singleton instance service.
		* Custom Settins from `appSettings.json` are retrieved through `IOptions` and defined as singleton services.
		* Above services above are dependency injected where needed.
* Notes
	* All DateTimes are stored as UTC in db and they will be converted to user’s local timezone for default application use.
* Stack used
	* Docker via Docker-Compose running a service container with:
		* Backend:
			* .NET Core 6 WebApp
		* Frontend:
		    * JS, HTML, CSS
	* Db: MongoDB Atlas (Multi-Cloud DB Service)
		* CRUD ops are done from containerised app through Repository pattern.
* DevOps
	* CI
		* Validation for PRs into main:
			* App restore & build through dotnet actions.
			* Secrets check through GitGuardian

### ⏭️ Next Works

* Functionality:
    * Modification of Clocking Information for a specific day.
    * Current settings modified through `appsettings.json` to be modified through web app Settings section.
        * This will allow current app settings to be modified instantly rather than changing them through `appsettings.json`
        * This will be done through an additional mongoDB collection within existing db.
    * Introduction of Topics. A topic can be added to a clocking so that on the same day you can track working times for different topics/themes of work. When looking back at data, you’ll be able to see in what topics you worked on together with working & resting information.
    * Insights section that show summaries, averages, trends and some other information depending on timeframe selected (Last week, Last month, Last year, …).
* Stack:
    * Separate data operations implementation from this app by creating an additional .NET Core API so that endpoints can be used from the web app. This API would run as an additional container service, eventually hosted in the cloud.
* DevOps:
    * Following actions to be added to the CI/CD pipeline:
        * When PR from release wants to merge main:
            * Add execution of xUnit testing available from source code through `dotnet`.
            * Add device & architecture compatibility testing.
        * When PR from release branch is merged into main:
            * Create tag & release.
            * Publish newest pushed container image in `GHCR.io` and make it as an available package to be used on-demand.
    * Add dependabot with the above CI/CD pipeline implemented so that versions can be bumped with a peace of mind that code has been tested, restored & build.
* Bug fixes & overall improvements.
* UI improvements.
