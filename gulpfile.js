/// <binding />
"use strict";

var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    htmlmin = require("gulp-htmlmin"),
    uglify = require("gulp-uglify"),
    merge = require("merge-stream"),
    del = require("del"),
    exec = require('child_process').exec,
    spawn = require("cross-spawn"),
    sass = require("gulp-sass"),
    autoprefixer = require("gulp-autoprefixer");

var jsVendorFiles = [
    'lib/jquery/dist/jquery.js',
    'lib/noty/js/noty/packaged/jquery.noty.packaged.js',
    'lib/jquery-tokenize/jquery.tokenize.js',
    'lib/knockout/knockout.js',
    'wwwsrc/js/nav.js'
];

var validationFiles = [
    'lib/jquery-validation/dist/jquery.validate.js',
    'lib/jquery-validation/dist/additional-methods.js',
    'lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js'
];

gulp.task("webpack", function (cb) {
    exec('webpack', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});

gulp.task("vendorJs", function () {
    return gulp.src(jsVendorFiles)
        .pipe(concat('vendor.js'))
        .pipe(uglify())
        .pipe(gulp.dest('wwwroot/dist/js'));
});

gulp.task("validationJs", function () {
    return gulp.src(validationFiles)
        .pipe(concat('validation.js'))
        .pipe(uglify())
        .pipe(gulp.dest('wwwroot/dist/js'));
});

gulp.task("sass:prod", function () {
    del('wwwroot/dist/css/*.css');
    return merge(
        gulp.src('wwwsrc/css/skeleton/*.scss')
          .pipe(sass().on('error', sass.logError)),
        gulp.src('lib/font-awesome/css/font-awesome.css'),
        gulp.src('lib/skeleton/css/normalize.css'),
        gulp.src('lib/skeleton/css/skeleton.css'),
        gulp.src('lib/jquery-tokenize/jquery.tokenize.css')
        )
      .pipe(autoprefixer({
          browsers: ['last 2 versions'],
          cascade: false
      }))
      .pipe(concat('site.min.css'))
      .pipe(cssmin())
      .pipe(gulp.dest('wwwroot/dist/css'));
});

gulp.task("watch:sass", function () {
    gulp.watch('wwwsrc/css/skeleton/*.scss', ["sass:prod"]);
});