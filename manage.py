import subprocess
import yaml
import argparse

parser = argparse.ArgumentParser()
parser.add_argument('action', help='run | build | push')
parser.add_argument('env', help='dev | prod')

args = parser.parse_args()

app_config_file = open('./config.yml')
app_config_file_contents = app_config_file.read()
app_config = yaml.safe_load(app_config_file_contents)
app_config_file.close()

print(app_config_file_contents)


def process_template(src: str, dst: str):
    template_file = open(src)
    template_file_contents = template_file.read()
    template_file.close()

    for k in app_config['env'][args.env]:
        replace_value = app_config['env'][args.env][k]

        template_file_contents = template_file_contents.replace(
            f'#{{{k}}}', f'{replace_value}')

    # Write template
    ok_config_file = open(dst, 'w+')
    ok_config_file.write(template_file_contents)
    ok_config_file.close()


# Process template for:
process_template('./templates/docker-compose-template.yml',
                 './docker-compose.yml')
process_template('./templates/appsettings-admin-template.json',
                 './IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.Admin/appsettings.json')
process_template('./templates/appsettings-sts-template.json',
                 './IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.STS.Identity/appsettings.json')
process_template('./templates/identitydata-template.json',
                 './IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.Admin/identitydata.json')


steps = [
    ('docker build -f Dockerfile.is4admin -t dotnetis4admin .', '.'),
    ('docker build -f Dockerfile.is4sts -t dotnetis4sts .', '.'),
    ('docker build -f Dockerfile.app -t dotnetapp .', '.'),
    ('docker compose up', '.'),
]

for (s, cwd) in steps:
    subprocess.run(s, cwd=cwd, shell=True)
