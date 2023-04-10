const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyPlugin = require("copy-webpack-plugin");

const isProduction = process.argv.find((v) => v.includes('production'));
console.log('Bundling for ' + (isProduction ? 'production' : 'development') + '...');

const plugins = [
  new MiniCssExtractPlugin({ filename: '[name].css' }),
  new HtmlWebpackPlugin({
    template: path.resolve(`./content/index.dev.html`),
  }),
  new CopyPlugin({
    patterns: [
      { from: "content/custom-component.js", to: "custom-component.js" }
    ]
  }),    
];

module.exports = {
  devtool: isProduction ? false : 'source-map',
  mode: isProduction ? 'production' : 'development',
  entry: {
    'testrouter3-app': './src/App.fs.js',
  },
  output: {
    path: path.join(__dirname, './dist'),
    filename: '[name].js',
  },
  devServer: {
    static: {
      directory: path.resolve(__dirname, './dist'),
    },
    port: 8080,
    devMiddleware: {
      writeToDisk: true
    }
  },
  module: {
    rules: [
      {
        test: /\.(css|sass|scss)$/i,
        use: [MiniCssExtractPlugin.loader, 'css-loader', 'postcss-loader', 'sass-loader'],
      },
    ],
  },
  plugins: plugins,
  resolve: {
    alias: {
      react: 'preact/compat',
      'react-dom': 'preact/compat',
    },
  },
};
