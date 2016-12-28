'use strict';

const webpack = require("webpack");

module.exports = {
    context: __dirname + "/wwwsrc/js",
    entry: {
        dashboard: "./modules/dashboard/main.js",
        daylist: "./modules/daylist/main.js",
        editPractice: "./modules/editPractice/main.js",
        manageAccount: "./modules/manageAccount/main.js",
        sendSMS: "./modules/sendSMS/main.js",
        payment: "./modules/payment/main.js",
        maps: "./maps/main.js"
    },
    output: {
        path: __dirname + "/wwwroot/dist/js/modules",
        filename: "[name].bundle.js"
    },
    plugins: [
        new webpack.ProvidePlugin({
            gmaps: __dirname + "/lib/google-maps/lib/Google.js",
            nm: __dirname + "/lib/notyMessage/notyMessage.js"
        }),
        new webpack.optimize.CommonsChunkPlugin({
            name: "commons",
            filename: "commons.js",
            minChunks: 2
        })
    ],
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: 'babel',
                query: {
                    presets: ['es2015']
                }
            }
        ]
    }
};