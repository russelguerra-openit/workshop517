// site.js

(function () {
	"use strict";

	var app = angular.module("app-site", []);
	
	app.controller("controller",
		function ($scope, $http) {

			$scope.title = "Song App";
			$scope.songs = [];
			$scope.editRef;
			$scope.addmodel = {};
			$scope.editmodel = {};
			$scope.deleteRef;

			$scope.mode = '';

			$http.get("/api/songs")
				.then(function (response) {
					$scope.songs = response.data;
					$scope.mode = 'table';
				},
					function () {
						console.log("Failed");
					});

			$scope.create = function () {
				$scope.addmodel = {};
				$scope.mode = 'add';
			}

			$scope.add = function (song) {
				$http.post("/api/songs", song)
					.then(function (response) {
						$scope.songs.push(response.data);
						$scope.mode = 'table';
					},
						function () {
							console.log("Failed");
						});
			}

			$scope.edit = function (song) {
				$scope.editRef = song;
				$scope.editmodel = angular.copy(song);
				$scope.mode = 'edit';
			}

			$scope.update = function (song) {
				$http.put("/api/songs/" + song.id, song)
					.then(function (response) {
						angular.copy(response.data, $scope.editRef);
						$scope.mode = 'table';
					},
						function () {
							console.log("Failed");
						});
			}

			$scope.delete = function (song) {
				console.log(song);
				$http.delete("/api/songs/" + song.id)
					.then(function (response) {
						$http.get("/api/songs")
							.then(function (response) {
								$scope.songs = response.data;
								$scope.mode = 'table';
							},
								function () {
									console.log("Failed");
								});
					},
					function () {
						console.log("Failed");
					});
			};
		}
	);

})();