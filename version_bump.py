import json
import sys

bump_type = 'patch'
if '--minor' in sys.argv:
    bump_type = 'minor'
elif '--major' in sys.argv:
    bump_type = 'major'

with open('package.json', 'r+', encoding='utf-8') as f:
    data = json.load(f)
    version = data['version'].split('.')

    if bump_type == 'patch':
        version[2] = str(int(version[2]) + 1)
    elif bump_type == 'minor':
        version[1] = str(int(version[1]) + 1)
        version[2] = '0'
    elif bump_type == 'major':
        version[0] = str(int(version[0]) + 1)
        version[1] = version[2] = '0'

    data['version'] = '.'.join(version)
    f.seek(0)
    json.dump(data, f, indent=2)
    f.truncate()

print(f"✅ {bump_type.capitalize()} version bumped to {data['version']}")
