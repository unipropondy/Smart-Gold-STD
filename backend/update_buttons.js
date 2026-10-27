const fs = require('fs');
const path = require('path');

const appDir = 'e:/Gold/smartgold/app';
const files = fs.readdirSync(appDir).filter(f => f.endsWith('.tsx'));

for (const file of files) {
  const filepath = path.join(appDir, file);
  let content = fs.readFileSync(filepath, 'utf8');

  let changed = false;

  // Replace pencil icon
  const pencilRegex = /<Ionicons\s+name="pencil-outline"[^>]*\/>/g;
  if (pencilRegex.test(content)) {
    content = content.replace(pencilRegex, 
      `<Ionicons name="pencil-outline" size={16} color={GOLD_COLOR} />\n          <ThemedText style={{ color: GOLD_COLOR, fontSize: 12, marginLeft: 4, fontWeight: '600' }}>Edit</ThemedText>`);
    changed = true;
  }

  // Replace trash icon
  const trashRegex = /<Ionicons\s+name="trash-outline"[^>]*\/>/g;
  if (trashRegex.test(content)) {
    content = content.replace(trashRegex, 
      `<Ionicons name="trash-outline" size={16} color="#F44336" />\n          <ThemedText style={{ color: '#F44336', fontSize: 12, marginLeft: 4, fontWeight: '600' }}>Delete</ThemedText>`);
    changed = true;
  }

  // Update actionButton style to have flex row
  const actionBtnRegex = /actionButton:\s*\{([^}]*)\}/g;
  content = content.replace(actionBtnRegex, (match, p1) => {
    if (!p1.includes('flexDirection')) {
      changed = true;
      return `actionButton: {${p1}, flexDirection: 'row', alignItems: 'center' }`;
    }
    return match;
  });

  if (changed) {
    fs.writeFileSync(filepath, content, 'utf8');
    console.log('Updated ' + file);
  }
}
