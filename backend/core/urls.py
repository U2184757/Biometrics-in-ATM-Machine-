from django.urls import path

from .views import * 

app_name = 'core'

urlpatterns = [

    path('login/',Login,name='login'),
    path('FaceRegognation/',FaceAuthentication,name='FaceRegognation'),
    path('submitFace/',submitFace,name='submitFace'),
    path('submit_otp/',submit_otp,name='submit_otp'),
    path('api_send_otp/',api_send_otp,name='api_send_otp'),
    path('api_check_balance/',api_check_balance,name='api_check_balance'),
    path('getFingerprint/',getFingerprint,name='getfingerprint'),
    path('deductbalance/',deductBalance,name='deductbalance'),


    path('FingerPrintAuth/',FingerPrintAuth,name='FingerPrintAuth'),
]