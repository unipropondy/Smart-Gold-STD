const fs = require('fs');
const path = require('path');

const appDir = 'e:/Gold/smartgold/app';
const files = fs.readdirSync(appDir).filter(f => f.endsWith('.tsx'));

for (const file of files) {
  const filepath = path.join(appDir, file);
  let content = fs.readFileSync(filepath, 'utf8');
  let changed = false;

  // Revert pencil icon and text
  const pencilRegex = /<Ionicons\s+name="pencil-outline"\s+size=\{16\}[^>]*\/>\s*<ThemedText[^>]*>Edit<\/ThemedText>/g;
  if (pencilRegex.test(content)) {
    content = content.replace(pencilRegex, `<Ionicons name="pencil-outline" size={20} color={GOLD_COLOR} />`);
    changed = true;
  }

  // Revert trash icon and text
  const trashRegex = /<Ionicons\s+name="trash-outline"\s+size=\{16\}[^>]*\/>\s*<ThemedText[^>]*>Delete<\/ThemedText>/g;
  if (trashRegex.test(content)) {
    content = content.replace(trashRegex, `<Ionicons name="trash-outline" size={20} color="#F44336" />`);
    changed = true;
  }

  // Revert actionButton style
  const actionBtnRegex = /actionButton:\s*\{([^}]*)flexDirection:\s*'row',\s*alignItems:\s*'center',?\s*([^}]*)\}/g;
  if (actionBtnRegex.test(content)) {
    content = content.replace(actionBtnRegex, (match, p1, p2) => {
      let combined = (p1 + p2).trim();
      // Remove trailing comma if exists
      if (combined.endsWith(',')) combined = combined.slice(0, -1);
      return `actionButton: { ${combined} }`;
    });
    changed = true;
  }
  
  // also handle the specific case where padding might have had a trailing comma
  const actionBtnRegex2 = /actionButton:\s*\{\s*padding:\s*(\d+),\s*flexDirection:\s*'row',\s*alignItems:\s*'center',?\s*\}/g;
  if (actionBtnRegex2.test(content)) {
    content = content.replace(actionBtnRegex2, `actionButton: {\n    padding: $1,\n  }`);
    changed = true;
  }

  if (changed) {
    fs.writeFileSync(filepath, content, 'utf8');
    console.log('Reverted ' + file);
  }
}
