const posthtml = require('posthtml');
const include  = require('posthtml-include');
const fs       = require('fs');
const path     = require('path');

const input  = path.join(__dirname, 'src', 'index.html');
const output = path.join(__dirname, 'index.html');

const html = fs.readFileSync(input, 'utf-8');

posthtml([ include({ root: path.join(__dirname, 'src') }) ])
  .process(html)
  .then(result => {
    fs.writeFileSync(output, result.html, 'utf-8');
    console.log(`Built index.html (${result.html.length} chars)`);
  })
  .catch(err => {
    console.error('Build failed:', err.message);
    process.exit(1);
  });
