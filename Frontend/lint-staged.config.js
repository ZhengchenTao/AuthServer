module.exports = {
  'src/**/*.{ts,tsx}': ['tslint --fix -p tsconfig.json', 'git add'],
  'types/**/*.ts': ['tslint --fix -p tsconfig.json', 'git add'],
  'src/**/*.scss': ['stylelint --fix', 'git add']
};
