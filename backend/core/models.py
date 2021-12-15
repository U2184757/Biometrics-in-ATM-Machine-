from django.db import models

# Create your models here.

authentication = (
    ('1','FACE AUTHENTICATION'),
    ('2','FINGER PRINT AUTHENTICATION'),
    ('3','PHONE AUTHENTICATION'),
)

class UserDetails(models.Model):
    account_number = models.CharField(primary_key=True,max_length=100,null=False,blank=False)
    profile_photo = models.ImageField(blank=True)
    phone_number = models.CharField(blank=True,max_length=20,null=True)
    otp = models.CharField(max_length=10,null=True,blank=True)
    balance = models.FloatField(default=0)
    finger_photo = models.ImageField(blank=True)
    last_authentication = models.CharField(choices=authentication,max_length=1,null=True,blank=True)
    auth_access = models.CharField(max_length=10,null=True,blank=True)

    def __str__(self):
        return self.profile_photo.url

    def serializer(self):
        return {
            "account_number":self.account_number,
            "phone_number":self.phone_number,
            "balance":self.balance,
            "last_authentication":self.last_authentication
        }