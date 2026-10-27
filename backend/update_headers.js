const fs = require('fs');
const path = require('path');

const appDir = 'e:/Gold/smartgold/app';
const files = fs.readdirSync(appDir).filter(f => f.endsWith('.tsx'));

for (const file of files) {
  const filepath = path.join(appDir, file);
  let content = fs.readFileSync(filepath, 'utf8');
  let changed = false;

  // Replace ACTIONS with EDIT and DELETE
  const actionsRegex = /<ThemedText[^>]*>ACTIONS<\/ThemedText>/g;
  if (actionsRegex.test(content)) {
    content = content.replace(actionsRegex, `<ThemedText style={[styles.headerText, { color: subtitleColor, marginRight: 16 }]}>EDIT</ThemedText><ThemedText style={[styles.headerText, { color: subtitleColor }]}>DELETE</ThemedText>`);
    changed = true;
  }

  // Also replace 'ACTIONS' if it's somehow different
  const actionsRegex2 = /<ThemedText[^>]*>ACTIONS<\/ThemedText>/g;
  
  // Increase actionCell width if it's 80
  const actionCellRegex = /actionCell:\s*\{\s*flexDirection:\s*'row',\s*justifyContent:\s*'flex-end',\s*width:\s*80\s*\}/g;
  if (actionCellRegex.test(content)) {
    content = content.replace(actionCellRegex, `actionCell: { flexDirection: 'row', justifyContent: 'flex-end', width: 120 }`);
    changed = true;
  }

  // Handle dailyclassrate actionCell (which has gap: 10)
  const actionCellRegex3 = /actionCell:\s*\{\s*flexDirection:\s*'row',\s*flex:\s*1,\s*gap:\s*10\s*\}/g;
  if (actionCellRegex3.test(content)) {
    content = content.replace(actionCellRegex3, `actionCell: { flexDirection: 'row', flex: 1, justifyContent: 'flex-end', gap: 16 }`);
    changed = true;
  }

  if (changed) {
    fs.writeFileSync(filepath, content, 'utf8');
    console.log('Updated ' + file);
  }
}
