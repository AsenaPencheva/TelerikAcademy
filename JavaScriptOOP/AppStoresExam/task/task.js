function solve() {

	const validator = {
		ValidateString: function (x) {
			if (typeof x !== 'string') {
				throw Error();
			}
		},
		ValidateName: function (x) {
			if (x.length < 1 || x.length > 24 || x.match(/[^0-9a-zA-Z ]/)) {
				throw Error();
			}
		},
		ValidatePositiveNumber: function (x) {
			if (!(x > 0)) { // with 0?
				throw Error();
			}
		},
		ValidateNumberInRange: function (x) {
			if (typeof x !== 'number' || x < 1 || x > 10) {
				throw Error();
			}
		},
		ValidateHostName: function (x) {
			if (x.length < 1 || x.length > 32) {
				throw Error();
			}
		},
		ValidateApps: function (x) {
			x.forEach(y => {
				if (!(y instanceof App)) {
					throw Error();
				}
			});

		},
	}

	class App {
		constructor(name, description, version, rating) {
			this.name = name;
			this.description = description;
			this.version = version;
			this.rating = rating;
		}

		get name() {
			return this._name;
		}
		set name(x) {
			validator.ValidateString(x);
			validator.ValidateName(x);
			this._name = x;
		}
		get description() {
			return this._description;
		}
		set description(x) {
			validator.ValidateString(x);
			this._description = x;
		}
		get version() {
			return this._version;
		}
		set version(x) {
			validator.ValidatePositiveNumber(x);
			this._version = x;
		}
		get rating() {
			return this._rating;
		}
		set rating(x) {
			validator.ValidateNumberInRange(x);
			this._rating = x;
		}

		release(arg) { // uslovieto?!

			if (typeof arg === 'number') {

				validator.ValidatePositiveNumber(arg);
				if (this._version >= arg) {
					throw Error();
				}
				this._version = arg;

			}

			else if (typeof arg === 'object') {

				validator.ValidatePositiveNumber(arg.version);

				if (this._version >= arg.version) {
					throw Error();
				}
				if (typeof arg.description !== 'undefined') {
					validator.ValidateString(arg.description);
					this._description = (arg.description);
				}
				if (typeof arg.rating !== 'undefined') {
					validator.ValidateNumberInRange(arg.rating);
					this._rating = arg.rating;
				}
				this._version = arg.version;


			}
			else {
				throw Error();
			}

			return this;
		}

	}

	class Store extends App {
		constructor(name, description, version, rating) {
			super(name, description, version, rating);
			this.apps = [];
		}

		get apps() {
			return this._apps;
		}
		set apps(x) {
			this._apps = x;
		}

		uploadApp(app) {
			if (!(app instanceof App)) {
				throw Error();
			}
			const index = this._apps.findIndex(x => x.name === app.name);
			if (index < 0) {
				this._apps.push(new App(app.name, app.description, app.version, app.rating));
				return this;
			}
			this._apps[index].release(app);
			const uploadedApp = this._apps[index];
			this._apps.splice(index, 1);
			this._apps.push(uploadedApp);
			return this;
		}

		takedownApp(name) {
			const index = this._apps.findIndex(x => x.name === name);
			if (index < 0) {
				throw Error();
			}
			this._apps.splice(index, 1);
			return this;
		}

		search(pattern) {
			return this._apps.filter(x => x.name.toLowerCase().includes(pattern.toLowerCase())).
				sort((x, y) => x.name.localeCompare(y.name));
		}

		listMostRecentApps(count) {
			count = (typeof count !== 'undefined') ? count : 10;
			const recentApps = this._apps.reverse().slice(0, count);

			return recentApps;
		}

		listMostPopularApps(count) {
			count = (typeof count !== 'undefined') ? count : 10;

			return this._apps.sort((x, y) => {

				y.rating - x.rating
			}).slice(0, count);
		}

	}

	class Device {
		constructor(hostname, apps) {
			this.hostname = hostname;
			this.apps = apps;
		}

		get hostname() {

			return this._hostname;
		}
		set hostname(x) {
			validator.ValidateString(x);
			validator.ValidateHostName(x);
			this._hostname = x;
		}
		get apps() {
			return this._apps;
		}
		set apps(x) {
			validator.ValidateApps(x);
			this._apps = x;

		}

		search(pattern) {
			const installedApps = {};
			this._apps.forEach(x => {
				if (x instanceof Store) {
					const arr = x.search(pattern);
					arr.forEach(y => {
						if (installedApps.hasOwnProperty(y.name)) {
							if (installedApps[y.name].version < y.version) {
								installedApps[y.name] = y;
							}
						}
						else {
							installedApps[y.name] = y;
						}
					});
				}
			});
			return Object.keys(installedApps)
				.map(key => installedApps[key])
			sort((x, y) => x.name.localeCompare(y.name));

		}

		install(name) {

			const arr = this.search(name)
			const app = arr.find(x => x.name === name);
			if (typeof app === 'undefined') {
				throw Error();
			}
			const index = this._apps.findIndex(x => x.name === name)

			if (index === -1) {
				this._apps.push(app);
			}


			return this;
		}

		uninstall(name) {
			this._apps.forEach(x => {
				if (!(x instanceof Store)) {
					if (x.name !== name) {
						throw Error();
					}
					const index = this._apps.indexOf(x);
					this._apps.splice(index, 1);
				}
			});
			return this;
		}

		listInstalled() {
			return this._apps
				//	.filter(x => !(x instanceof Store))
				.sort((x, y) => x.name.localeCompare(y.name));
		}

		update() {
			this._apps.forEach(x => {
				const arr = this.search(x.name);

				const app = arr.find(y => y === x.name);


				if (app instanceof App) {
					if (app.version > x.version) {
						x.version = app.version;
					}

				}

			});
			return this;
		}
	}

	return {
		createApp(name, description, version, rating) {
			return new App(name, description, version, rating);
		},
		createStore(name, description, version, rating) {
			return new Store(name, description, version, rating);
		},
		createDevice(hostname, apps) {
			return new Device(hostname, apps);
		}
	};
}

// Submit the code above this line in bgcoder.com
module.exports = solve;