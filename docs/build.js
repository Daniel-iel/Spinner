#!/usr/bin/env node

const posthtml = require('posthtml');
const include = require('posthtml-include');
const fs = require('fs');
const path = require('path');
const { execSync } = require('child_process');

const buildDir = path.join(__dirname, 'build');
const srcDir = path.join(__dirname, 'src');
const scssDir = path.join(__dirname, 'scss');
const srcHTML = path.join(srcDir, 'index.html');
const outHTML = path.join(buildDir, 'index.html');
const outCSSDir = path.join(buildDir, 'css');
const outCSS = path.join(outCSSDir, 'main.css');
const inCSS = path.join(scssDir, 'main.scss');

// Criar diretório build
if (!fs.existsSync(buildDir)) {
  fs.mkdirSync(buildDir, { recursive: true });
}
if (!fs.existsSync(outCSSDir)) {
  fs.mkdirSync(outCSSDir, { recursive: true });
}

// 1. Compilar SCSS → CSS
console.log('🎨 Compiling SCSS...');
try {
  execSync(`npx sass "${inCSS}" "${outCSS}" --style=compressed`, { stdio: 'inherit' });
  console.log(`✅ CSS built: ${outCSS}`);
} catch (err) {
  console.error('❌ SCSS compilation failed');
  process.exit(1);
}

// 2. Processar HTML com posthtml-include
console.log('\n📝 Processing HTML...');
try {
  const html = fs.readFileSync(srcHTML, 'utf-8');
  
  posthtml([
    include({ root: srcDir })
  ])
    .process(html)
    .then(result => {
      fs.writeFileSync(outHTML, result.html, 'utf-8');
      console.log(`✅ HTML built: ${outHTML} (${result.html.length} bytes)`);
      console.log('\n✨ Documentation built successfully!');
    })
    .catch(err => {
      console.error('❌ HTML processing failed:', err.message);
      process.exit(1);
    });
} catch (err) {
  console.error('❌ Build failed:', err.message);
  process.exit(1);
}
