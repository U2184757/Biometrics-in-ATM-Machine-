
from django.http.response import JsonResponse
from twilio.rest import Client
import random
from .models import *
from django.http import JsonResponse

def phone_authentication(account_id):
    user = UserDetails.objects.get(account_number=account_id)
    phone_num = user.phone_number
    otp = random.randint(1111,9999)
    account_sid = 'AC2e89dd9128f5d4b33e25b94276776be2'
    auth_token = 'cd682808f08e597ea9d973d2ed5aa51b'
    client = Client(account_sid, auth_token)

    message = client.messages.create(
                body=f"Your OTP is {otp}",
                from_='+17163254852',
                to=phone_num
                )
    user.otp = otp
    user.save()
    return True



def confirm_otp(account_id,otp):
    user = UserDetails.objects.get(account_number=account_id)
    if user.otp == otp:
        user.last_authentication = 3
        user.save()
        return True
    else:
        return False