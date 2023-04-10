const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

const isProduction = process.argv.find((v) => v.includes('production'));
console.log('Bundling for ' + (isProduction ? 'production' : 'development') + '...');

const plugins = [];

if (!isProduction) {
  plugins.push(
    new HtmlWebpackPlugin({
      template: path.resolve('./content/index.dev.html'),
    })
  );
}

module.exports = {
  devtool: isProduction ? 'production' : 'source-map',
  mode: isProduction ? 'production' : 'development',
  entry: {
    'custom-component': './src/App.fs.js',
  },
  output: {
    path: path.join(__dirname, './dist'),
    filename: '[name].js',
  },
  devServer: {
    contentBase: path.resolve(__dirname, './dist'),
    port: 8082,
  },
  module: {
    rules: [
      {
        test: /\.(css|sass|scss)$/i,
        use: [MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader'],
      },
    ],
  },
  plugins: plugins,
  resolve: {
    alias: {
      react: 'preact/compat',
      'react-dom': 'preact/compat',
    },
  }
};
