# Generated by Django 2.2.13 on 2021-12-07 14:41

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('core', '0006_auto_20211105_0727'),
    ]

    operations = [
        migrations.AddField(
            model_name='userdetails',
            name='auth_access',
            field=models.CharField(blank=True, max_length=10, null=True),
        ),
    ]